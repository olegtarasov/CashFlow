import React from 'react';
import {Route, Routes} from 'react-router-dom';
import {Layout} from './components/Layout';
import {ErrorContext, useErrorState} from "./context/errorContext";
import {MontlySpending} from "./components/MonthlySpending";

export function App() {
    const errorState = useErrorState();

    return (
        <ErrorContext.Provider value={errorState}>
            <Routes>
                <Route exact path='/' element={<Layout/>}>
                    <Route index element={<MontlySpending/>}/>
                </Route>
            </Routes>
        </ErrorContext.Provider>
    )
}