import {createContext, useState, useContext} from 'react'
import {isString, has} from "lodash"

export const ErrorContext = createContext({});

export function useErrorState() {
    const [errorText, setErrorText] = useState("");

    function processError(error) {
        if (!error)
            return;
        if (isString(error))
            setErrorText(error);
        else if (has(error, "response.data.detail"))
            setErrorText(error.response.data.detail);
        else
            setErrorText(String(error));
    }

    return {errorText, processError}
}

export function useErrorContext() {
    return useContext(ErrorContext);
}