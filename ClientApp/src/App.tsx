import React from 'react';
import {Route, Routes} from 'react-router-dom';
import {Layout} from './Layout';
import {GlobalErrorContext, useErrorState} from "./context/errorContext";
import {Spending} from "./pages/Spending";

export function App() {
    const errorState = useErrorState();

    return (
        <GlobalErrorContext.Provider value={errorState}>
            <Routes>
                <Route path='/' element={<Layout/>}>
                    <Route index element={<Spending/>}/>
                </Route>
            </Routes>
        </GlobalErrorContext.Provider>
    )
}