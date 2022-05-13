import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

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

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
