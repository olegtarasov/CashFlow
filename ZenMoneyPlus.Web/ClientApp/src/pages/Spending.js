import React, {useEffect, useState} from 'react';
import {useErrorContext} from "../context/errorContext";
import axios from "axios";
import DropdownTreeSelect from 'react-dropdown-tree-select';
import 'react-dropdown-tree-select/dist/styles.css';
import Highcharts from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import {SpendingFilter} from "../components/SpendingFilter";
import {Container, Row} from "reactstrap";

function mapTag(tag) {
    return {label: tag.title, value: tag.id, children: tag.childrenTags.map(mapTag), checked: true};
}

export function Spending() {
    const errorContext = useErrorContext();
    const [tags, setTags] = useState({});
    const [selectedTags, setSelectedTags] = useState([]);
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
            <SpendingFilter/>
            <DropdownTreeSelect className="my-2" data={tags} onChange={onSelectedTagsChange}/>
            <HighchartsReact className="my-2" highcharts={Highcharts} options={barOptions}/>
        </>
    );
}
