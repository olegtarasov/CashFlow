import React, {useRef, useState} from 'react';
import Picker from "react-month-picker";
import "react-month-picker/css/month-picker.css";
import {Input} from "reactstrap";

const pickerLang = {
    months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    from: 'From', to: 'To',
}

export type MonthAndYear = {
    month: number,
    year: number
};

export type MonthRange = {
    from: MonthAndYear,
    to: MonthAndYear
}

export type MonthPickerProps = {
    onChange: (value: MonthRange) => void,
    initialRange: MonthRange
}

function MonthPicker({onChange, initialRange}: MonthPickerProps) {
    const [text, setText] = useState(formatRange(initialRange));
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
            value={initialRange}
            lang={pickerLang}
            onDismiss={handleChange}
            years={5}
        >
            <Input readOnly value={text} onClick={() => monthPicker.current.show()}
                   style={{marginLeft: "-1px", borderTopLeftRadius: 0, borderBottomLeftRadius: 0}}/>
        </Picker>
    );
}

// MonthPicker.propTypes = {
//     onChange: PropTypes.func.isRequired,
//     initialRange: PropTypes.shape({
//         from: PropTypes.shape({year: PropTypes.number, month: PropTypes.number}),
//         to: PropTypes.shape({year: PropTypes.number, month: PropTypes.number})
//     }).isRequired
// };

export {MonthPicker};