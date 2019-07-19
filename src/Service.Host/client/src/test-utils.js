import { render } from '@testing-library/react';
import { Provider } from 'react-redux';
import React from 'react';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import configureMockStore from 'redux-mock-store';
import { MemoryRouter } from 'react-router-dom';

// eslint-disable-next-line react/prop-types
const Providers = ({ children }) => {
    const mockStore = configureMockStore();
    const store = mockStore({});
    return (
        <Provider store={store}>
            <MuiThemeProvider theme={createMuiTheme()}>
                <MemoryRouter>{children}</MemoryRouter>
            </MuiThemeProvider>
        </Provider>
    );
};

const customRender = (ui, options) => render(ui, { wrapper: Providers, ...options });

export default customRender;
