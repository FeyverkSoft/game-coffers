import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import { store } from './_helpers';
import { MyApp } from './App.tsx';

ReactDOM.render(
    <Provider
        store={store}>
        <MyApp />
    </Provider>,
    document.getElementById('app')
);