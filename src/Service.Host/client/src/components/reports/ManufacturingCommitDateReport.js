import React, { Fragment } from 'react';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

const ManufacturingCommitDateReport = ({ reportData, loading, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                <Title
                    text={`Manufacturing Commit Date Results For ${moment(options.date).format('DD-MMM-YYYY')}`}
                />
            </Grid>
            <Grid item xs={6}></Grid>
            <Grid item xs={6} />
        </Grid>
    </Page>
);

ManufacturingCommitDateReport.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

ManufacturingCommitDateReport.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default ManufacturingCommitDateReport;
