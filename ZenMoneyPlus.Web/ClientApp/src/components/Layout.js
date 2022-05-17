import React from 'react';
import {Outlet} from "react-router-dom";
import {Container} from 'reactstrap';
import {NavMenu} from './NavMenu';

export default function Layout() {
    return (
        <>
            <NavMenu/>
            <Container>
                <Outlet/>
            </Container>
        </>
    );
}