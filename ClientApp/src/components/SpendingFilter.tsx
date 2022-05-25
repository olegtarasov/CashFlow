import React, {Dispatch, useCallback} from 'react';
import {Button, Form, Input, InputGroup, InputGroupText} from "reactstrap";
import {MonthPicker} from "./MonthPicker";
import {MonthRange, SpendingRequest, SpendingRequestReduceAction} from '../messages/Spending.Messages';
import {TagSelector} from "./TagSelector";


export interface SpendingFilterProps {
    requestDispatch: Dispatch<SpendingRequestReduceAction>;
    request: SpendingRequest;
}

export function SpendingFilter({requestDispatch, request}: SpendingFilterProps) {
    const onTagsChanged = useCallback((tags: string[]) => requestDispatch({
        type: "tags",
        tags: tags
    }), [requestDispatch]);
    const onMonthRangeChanged = useCallback((value: MonthRange) => requestDispatch({
        type: "monthRange",
        monthRange: value
    }), [requestDispatch]);

    return (
        <>
            <Form className="row my-2 align-items-center">
                <div className="col-auto">
                    <InputGroup>
                        {/* Buttons are being rerendered anyway, since the use request.mode as state,
                         no need to memoize event handler */}
                        <Button onClick={() => requestDispatch({type: "mode", mode: "month"})}
                                active={request.mode === "month"}>Monthly</Button>
                        <Button onClick={() => requestDispatch({type: "mode", mode: "year"})}
                                active={request.mode === "year"}>Yearly</Button>
                        {request.mode === "month"
                            ? <MonthPicker onChange={onMonthRangeChanged}/>
                            : (
                                <>
                                    <Input placeholder="Start year" value={request.yearRange.from}/>
                                    <InputGroupText>—</InputGroupText>
                                    <Input placeholder="End year" value={request.yearRange.to}/>
                                </>
                            )}
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