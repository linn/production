import React from 'react';
import { Grid } from '@material-ui/core';
import { Title } from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';

function NotFound() {
    return (
        <Page>
            <Grid container alignItems="flex-start" styles={{ alignItems: 'flex-start' }}>
                <Title text="Page not found" />
            </Grid>
        </Page>
    );
}

export default NotFound;
