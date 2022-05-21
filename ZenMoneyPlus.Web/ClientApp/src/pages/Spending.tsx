import React, {useCallback, useEffect, useReducer, useState} from 'react';
import {useErrorContext} from "../context/errorContext";
import axios from "axios";
import Highcharts, {Options} from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import {SpendingFilter, SpendingRequest, InitialSpendingRequest} from "../components/SpendingFilter";

export function Spending() {
    const errorContext = useErrorContext();
    const [request, setRequest] = useState<SpendingRequest>(InitialSpendingRequest);
    const onRequestChanged = useCallback((req: SpendingRequest) => setRequest(req), []);
    const [barOptions, setBarOptions] = useState<Options>({
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
                    'Total: ' + this.point.total;
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
        if (request.tags.length === 0)
            return;

        axios.post("api/Spending", request)
            .then(response => {
                setBarOptions({
                    xAxis: {
                        categories: response.data.categories
                    },
                    series: response.data.series
                });
            })
            .catch(error => {
                errorContext.processError(error);
            });
    }, [request, errorContext]);

    return (
        <>
            <SpendingFilter onRequestChanged={onRequestChanged}/>
            <HighchartsReact className="my-2" highcharts={Highcharts} options={barOptions}/>
        </>
    );
}
