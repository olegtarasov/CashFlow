import {createContext, useState, useContext} from "react"
import {isString, has} from "lodash"
import {AxiosError} from "axios";

export type ErrorContext = {
    errorText: string,
    processError: (error: string) => void
}

export const GlobalErrorContext = createContext<ErrorContext>({
    errorText: "", processError: _ => {
    }
});

export function useErrorState() {
    const [errorText, setErrorText] = useState("");

    function processError(error: string | AxiosError<{ errors: object[] }>) {
        if (!error)
            return;
        if (isString(error))
            setErrorText(error);
        else if (has(error, "response.data.errors"))
            setErrorText(JSON.stringify((error as AxiosError<{ errors: object[] }>).response!.data.errors));
        else if (has(error, "message"))
            setErrorText((error as Error).message);
        else
            setErrorText(JSON.stringify(error));
    }

    return {errorText, processError}
}

export function useErrorContext(): ErrorContext {
    return useContext(GlobalErrorContext);
}