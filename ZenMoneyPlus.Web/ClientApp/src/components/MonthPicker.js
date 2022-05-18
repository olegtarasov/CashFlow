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
    const [monthRange, setMonthRange] = useState(initialRange);
    const monthPicker = useRef();

    function handleChange(value) {
        setMonthRange(value);
        onChange(value);
    }

    function formatRange() {
        return `${monthRange.from.month}.${monthRange.from.year} — ${monthRange.to.month}.${monthRange.to.year}`;
    }

    return (
        <Picker
            ref={monthPicker}
            value={monthRange}
            lang={pickerLang}
            onDismiss={handleChange}
            years={5}
        >
            <Input readOnly value={formatRange()} onClick={() => monthPicker.current.show()}
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