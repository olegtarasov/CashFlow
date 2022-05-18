import React, {useCallback, useState} from 'react';
import {Button, ButtonGroup, Col, Form, Input, InputGroup, InputGroupText, Row} from "reactstrap";
import {MonthPicker} from "./MonthPicker";
import {TagSelector} from "./TagSelector";

function SpendingFilter({onChange}) {
    const [type, setType] = useState("month");
    const [selectedTags, setSelectedTags] = useState([]);
    const [monthRange, setMonthRange] = useState({from: {year: 2019, month: 1}, to: {year: 2022, month: 12}});

    return (
        <>
            <Form className="row my-2 align-items-center">
                <div className="col-auto">
                    <InputGroup>
                        <Button onClick={() => setType("month")} active={type === "month"}>Monthly</Button>
                        <Button onClick={() => setType("year")} active={type === "year"}>Yearly</Button>
                        <MonthPicker onChange={(value) => setMonthRange(value)} initialRange={monthRange}/>
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
            <TagSelector onChange={(tags) => setSelectedTags(tags)}/>
        </>
    );
}

export {SpendingFilter};