﻿import { render } from '@testing-library/react';
import { Provider } from 'react-redux';
import React from 'react';
import { MuiThemeProvider, createMuiTheme, ThemeProvider } from '@material-ui/core/styles';
import configureMockStore from 'redux-mock-store';
import { MemoryRouter } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';
import MomentUtils from '@date-io/moment';

const theme = createMuiTheme({
    spacing: 4,
    palette: {
        primary: {
            main: '#007bff'
        }
    }
});
// eslint-disable-next-line react/prop-types
const Providers = ({ children }) => {
    const mockStore = configureMockStore();
    const store = mockStore({});
    return (
        <Provider store={store}>
            <SnackbarProvider dense maxSnack={5}>
                <MuiThemeProvider theme={theme}>
                    <MemoryRouter>
                        <MuiPickersUtilsProvider utils={MomentUtils}>
                            {children}
                        </MuiPickersUtilsProvider>
                    </MemoryRouter>
                </MuiThemeProvider>
            </SnackbarProvider>
        </Provider>
    );
};

const customRender = (ui, options) => render(ui, { wrapper: Providers, ...options });

export default customRender;
