import React from 'react';
import {Outlet} from "react-router-dom";
import {Container} from 'reactstrap';
import {NavMenu} from './components/NavMenu';
import {ErrorAlert} from "./components/ErrorAlert";

export function Layout() {
    return (
        <>
            <NavMenu/>

            <div className="pt-3">
                <ErrorAlert/>
            </div>

            <Container>
                <Outlet/>
            </Container>
        </>
    );
}