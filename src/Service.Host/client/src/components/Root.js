import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Navigation, linnTheme } from '@linn-it/linn-form-components-library';
import ThemeProvider from '@material-ui/styles/ThemeProvider';
import PropTypes from 'prop-types';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <ThemeProvider theme={linnTheme}>
                        <Router history={history}>
                            <div>
                                <Navigation />
                                <CssBaseline />

                                <Route
                                    exact
                                    path="/"
                                    render={() => <Redirect to="/production/maintenance" />}
                                />

                                <Route
                                    exact
                                    path="/production"
                                    render={() => <Redirect to="/production/maintenance" />}
                                />

                                <Switch>
                                    <Route exact path="/production/maintenance" component={App} />

                                    <Route
                                        exact
                                        path="/production/maintenance/signin-oidc-client"
                                        component={Callback}
                                    />
                                </Switch>
                            </div>
                        </Router>
                    </ThemeProvider>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
