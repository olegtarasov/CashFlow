import {createContext, useState, useContext} from 'react'
import {isString, has, toString} from "lodash"

export const ErrorContext = createContext({});

export function useErrorState() {
    const [errorText, setErrorText] = useState("");

    function processError(error) {
        if (!error)
            return;
        if (isString(error))
            setErrorText(error);
        else if (has(error, "response.data.errors"))
            setErrorText(JSON.stringify(error.response.data.errors));
        else
            setErrorText(JSON.stringify(error));
    }

    return {errorText, processError}
}

export function useErrorContext() {
    return useContext(ErrorContext);
}