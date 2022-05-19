import React, {useCallback, useState} from 'react';
import {Button, ButtonGroup, Col, Form, Input, InputGroup, InputGroupText, Row} from "reactstrap";
import {MonthPicker} from "./MonthPicker";
import {TagSelector} from "./TagSelector";
import PropTypes from "prop-types";

function SpendingFilter({onChange, request}) {
    function updateMode(mode) {
        request = {...request, mode: mode};
        onChange(request);
    }

    function updateTags(tags) {
        request = {...request, tags: tags};
        onChange(request);
    }

    function updateMonthRange(value) {
        request = {...request, monthRange: value};
        onChange(request);
    }

    return (
        <>
            <Form className="row my-2 align-items-center">
                <div className="col-auto">
                    <InputGroup>
                        <Button onClick={() => updateMode("month")} active={request.mode === "month"}>Monthly</Button>
                        <Button onClick={() => updateMode("year")} active={request.mode === "year"}>Yearly</Button>
                        <MonthPicker onChange={updateMonthRange} initialRange={request.monthRange}/>
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
            <TagSelector onChange={updateTags}/>
        </>
    );
}

SpendingFilter.propTypes = {
    onChange: PropTypes.func.isRequired,
    request: PropTypes.shape({
        mode: PropTypes.string.isRequired,
        monthRange: MonthPicker.propTypes.initialRange,
        tags: PropTypes.array.isRequired
    }).isRequired
};

export {SpendingFilter};