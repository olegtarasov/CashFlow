import React, {useRef, useState} from 'react';
import Picker from 'react-month-picker';
import PropTypes from "prop-types";
import "react-month-picker/css/month-picker.css";
import {Input} from "reactstrap";

const pickerLang = {
    months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    from: 'From', to: 'To',
}

function MonthPicker({onChange, initialRange}) {
    const [text, setText] = useState(formatRange(initialRange));
    const monthPicker = useRef();

    function handleChange(value) {
        setText(formatRange(value));
        onChange(value);
    }

    function formatRange(value) {
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

MonthPicker.propTypes = {
    onChange: PropTypes.func.isRequired,
    initialRange: PropTypes.shape({
        from: PropTypes.shape({year: PropTypes.number, month: PropTypes.number}),
        to: PropTypes.shape({year: PropTypes.number, month: PropTypes.number})
    }).isRequired
};

export {MonthPicker};