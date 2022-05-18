import React, {useState} from 'react';
import {Button, ButtonGroup, Col, Form, Input, InputGroup, InputGroupText, Row} from "reactstrap";
import {MonthPicker} from "./MonthPicker";

function SpendingFilter() {
    const [monthRange, setMonthRange] = useState({from: {year: 2019, month: 1}, to: {year: 2022, month: 12}});

    return (
        <Form className="row my-2 align-items-center">
            <div className="col-auto">
                <InputGroup>
                    <Button>Monthly</Button>
                    <Button>Yearly</Button>
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

    );
}

export {SpendingFilter};