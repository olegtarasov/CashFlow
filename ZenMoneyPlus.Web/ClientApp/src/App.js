import React, {Component} from 'react';
import {Route, Routes} from 'react-router-dom';
import Layout from './components/Layout';
import {Home} from './components/Home';
import {FetchData} from './components/FetchData';
import {Counter} from './components/Counter';
import {NavMenu} from "./components/NavMenu";

// export function App() {
//     const errorState = useErrorState();
//     const loadedDataState = useLoadedDataState();
//
//     return (
//         <ErrorContext.Provider value={errorState}>
//             <LoadedDataContext.Provider value={loadedDataState}>
//                 <NavMenu/>
//
//                 <div className="pt-3">
//                     <ErrorAlert/>
//                 </div>
//
//                 <Container>
//                     <Route exact path='/'>
//                         <Explorer/>
//                     </Route>
//                     <Route path="/orderHistory">
//                         <OrderbookHistory/>
//                     </Route>
//                 </Container>
//
//             </LoadedDataContext.Provider>
//         </ErrorContext.Provider>
//     );
// }
//
export default function App() {
    return (
        <Routes>
            <Route exact path='/' element={<Layout/>}>
                <Route index element={<Home/>}/>
                <Route path='/counter' element={<Counter/>}/>
                <Route path='/fetch-data' element={<FetchData/>}/>
            </Route>
        </Routes>
    )
}