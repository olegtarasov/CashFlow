import React, {useEffect, useRef, useState} from 'react';
import {useErrorContext} from "../context/errorContext";
import axios from "axios";
import Picker from 'react-month-picker';
import "react-month-picker/css/month-picker.css";
import DropdownTreeSelect from 'react-dropdown-tree-select';
import 'react-dropdown-tree-select/dist/styles.css';
import Highcharts from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import {Button} from "reactstrap";

const pickerLang = {
    months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    from: 'From', to: 'To',
}

function mapTag(tag) {
    return {label: tag.title, value: tag.id, children: tag.childrenTags.map(mapTag), checked: true};
}

export function MontlySpending() {
    const errorContext = useErrorContext();
    const [tags, setTags] = useState({});
    const [selectedTags, setSelectedTags] = useState([]);
    const [monthRange, setMonthRange] = useState({from: {year: 2019, month: 1}, to: {year: 2022, month: 12}});
    const monthPicker = useRef();
    const [barOptions, setBarOptions] = useState({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Monthly spending by top-level categories'
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Amount, ₽'
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' +
                    this.series.name + ': ' + this.y + '<br/>' +
                    'Total: ' + this.point.stackTotal;
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal'
            }
        },
        series: []
    });

    useEffect(() => {
        axios.get("api/Tags?mode=outcome")
            .then(response => {
                const nodes = response.data.map(mapTag);
                setTags(nodes);
                onSelectedTagsChange(null, nodes);
            })
            .catch(error => {
                errorContext.processError(error);
            });
    }, [errorContext]);

    useEffect(() => {
        if (selectedTags.length === 0)
            return;

        axios.post("api/Monthly", {tags: selectedTags})
            .then(response => {

            })
            .catch(error => {
                errorContext.processError(error);
            });
    }, [selectedTags, errorContext]);

    function onSelectedTagsChange(currentNode, selectedNodes) {
        // currentNode: { label, value, children, expanded, checked, className, ...extraProps }
        // selectedNodes: [{ label, value, children, expanded, checked, className, ...extraProps }]
        setSelectedTags(selectedNodes.map(x => x.value));
    }


    return (
        <>
            <Picker
                ref={monthPicker}
                value={monthRange}
                theme="light"
                lang={pickerLang}
            >
                <Button onClick={() => monthPicker.current.show()}>Foobar</Button>
            </Picker>
            <DropdownTreeSelect data={tags} onChange={onSelectedTagsChange}/>
            <HighchartsReact highcharts={Highcharts} options={barOptions}/>
        </>
    );
}
