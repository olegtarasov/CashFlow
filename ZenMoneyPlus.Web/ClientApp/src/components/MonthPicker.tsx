import React, {useCallback, useRef, useState} from 'react';
import Picker from "react-month-picker";
import "react-month-picker/css/month-picker.css";
import {Input} from "reactstrap";
import {INITIAL_SPENDING_REQUEST} from "../messages/Spending.Messages";

const PICKER_LANG = {
    months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    from: 'From', to: 'To',
}

export interface MonthAndYear {
    month: number;
    year: number;
}

export interface MonthRange {
    from: MonthAndYear;
    to: MonthAndYear;
}

export interface MonthPickerProps {
    onChange: (value: MonthRange) => void;
}

export const MonthPicker = React.memo(function MonthPicker({onChange}: MonthPickerProps) {
    const [text, setText] = useState(formatRange(INITIAL_SPENDING_REQUEST.monthRange));
    const monthPicker = useRef<any>();

    function handleChange(value: MonthRange) {
        setText(formatRange(value));
        onChange(value);
    }

    function formatRange(value: MonthRange) {
        return `${value.from.month}.${value.from.year} — ${value.to.month}.${value.to.year}`;
    }

    return (
        <Picker
            ref={monthPicker}
            value={INITIAL_SPENDING_REQUEST.monthRange}
            lang={PICKER_LANG}
            onDismiss={handleChange}
            years={5}
        >
            <Input readOnly value={text} onClick={() => monthPicker.current.show()}
                   style={{marginLeft: "-1px", borderTopLeftRadius: 0, borderBottomLeftRadius: 0}}/>
        </Picker>
    );
});