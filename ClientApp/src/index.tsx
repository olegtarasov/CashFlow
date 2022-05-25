import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import {createRoot} from 'react-dom/client';
import {BrowserRouter} from 'react-router-dom';
import {App} from './App';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
if (baseUrl == null) {
    throw new Error("Failed to get baseUrl");
}

const rootElement = document.getElementById('root') as HTMLElement;
const root = createRoot(rootElement);
root.render(
    <BrowserRouter basename={baseUrl}>
        <App/>
    </BrowserRouter>
);
