import React, {useEffect, useState} from 'react';
import {useErrorContext} from "../context/errorContext";
import axios from "axios";
import Highcharts from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import {SpendingFilter} from "../components/SpendingFilter";
import {TagSelector} from "../components/TagSelector";

export function Spending() {
    const errorContext = useErrorContext();
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

    // useEffect(() => {
    //     if (selectedTags.length === 0)
    //         return;
    //
    //     axios.post("api/Spending", {tags: selectedTags})
    //         .then(response => {
    //
    //         })
    //         .catch(error => {
    //             errorContext.processError(error);
    //         });
    // }, [selectedTags, errorContext]);


    return (
        <>
            <SpendingFilter/>
            <HighchartsReact className="my-2" highcharts={Highcharts} options={barOptions}/>
        </>
    );
}
