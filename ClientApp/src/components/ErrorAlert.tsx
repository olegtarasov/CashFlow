import {Alert, Container} from "reactstrap";
import {useErrorContext} from "../context/errorContext";

export function ErrorAlert() {
    const errorContext = useErrorContext();

    return (
        <Container>
            <Alert color="danger" isOpen={errorContext.errorText != null && errorContext.errorText !== ""}
                   toggle={() => errorContext.processError("")}>
                {errorContext.errorText}
            </Alert>
        </Container>
    );
}