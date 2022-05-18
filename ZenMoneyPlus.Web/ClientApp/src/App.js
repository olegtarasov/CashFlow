import React from 'react';
import {Route, Routes} from 'react-router-dom';
import {Layout} from './Layout';
import {ErrorContext, useErrorState} from "./context/errorContext";
import {Spending} from "./pages/Spending";

export function App() {
    const errorState = useErrorState();

    return (
        <ErrorContext.Provider value={errorState}>
            <Routes>
                <Route exact path='/' element={<Layout/>}>
                    <Route index element={<Spending/>}/>
                </Route>
            </Routes>
        </ErrorContext.Provider>
    )
}