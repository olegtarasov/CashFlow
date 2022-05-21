import React, {useCallback, useEffect, useReducer} from 'react';
import {Button, Form, Input, InputGroup, InputGroupText} from "reactstrap";
import {MonthPicker, MonthRange} from "./MonthPicker";
import {TagSelector} from "./TagSelector";

export type SpendingMode = "month" | "year";

export type SpendingRequest = {
    mode: SpendingMode,
    monthRange: MonthRange,
    tags: string[]
};

export type SpendingRequestReduceAction =
    | { type: "mode", mode: SpendingMode }
    | { type: "monthRange", monthRange: MonthRange }
    | { type: "tags", tags: string[] };

function requestReducer(request: SpendingRequest, action: SpendingRequestReduceAction) {
    switch (action.type) {
        case "tags":
            return {...request, tags: action.tags};
        case "mode":
            return {...request, mode: action.mode};
        case "monthRange":
            return {...request, monthRange: action.monthRange};
        default:
            throw new Error("Unsupported request reducer action");
    }
}

export type SpendingFilterProps = {
    onRequestChanged: (request: SpendingRequest) => void,
};

export const InitialSpendingRequest: SpendingRequest = {
    mode: "month",
    monthRange: {from: {year: 2019, month: 1}, to: {year: 2022, month: 12}},
    tags: []
};

function SpendingFilter({onRequestChanged}: SpendingFilterProps) {
    const [request, requestDispatch] = useReducer(requestReducer, InitialSpendingRequest);
    const onTagsChanged = useCallback((tags: string[]) => requestDispatch({type: "tags", tags: tags}), []);

    useEffect(() => onRequestChanged(request), [request, onRequestChanged]);

    return (
        <>
            <Form className="row my-2 align-items-center">
                <div className="col-auto">
                    <InputGroup>
                        <Button onClick={() => requestDispatch({type: "mode", mode: "month"})}
                                active={request.mode === "month"}>Monthly</Button>
                        <Button onClick={() => requestDispatch({type: "mode", mode: "year"})}
                                active={request.mode === "year"}>Yearly</Button>
                        <MonthPicker onChange={(value) => requestDispatch({type: "monthRange", monthRange: value})}
                                     initialRange={request.monthRange}/>
                    </InputGroup>
                </div>
                <div className="col-auto">
                    <InputGroup>
                        <Input placeholder="Min price"/>
                        <InputGroupText>—</InputGroupText>
                        <Input placeholder="Max price"/>
                    </InputGroup>
                </div>
            </Form>
            <TagSelector onSelectedChanged={onTagsChanged}/>
        </>
    );
}

// SpendingFilter.propTypes = {
//     requestDispatch: PropTypes.func.isRequired,
//     request: PropTypes.shape({
//         mode: PropTypes.string.isRequired,
//         monthRange: MonthPicker.propTypes.initialRange,
//         tags: PropTypes.array.isRequired
//     }).isRequired
// };

export {SpendingFilter};