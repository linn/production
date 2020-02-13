import React, { Fragment } from 'react';
import { Loading, Title, MultiReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const Results = ({ reportData }) => (
    <>
        {reportData.length === 0 ? (
            <div>No results returned for selected parameters</div>
        ) : (
            <MultiReportTable
                reportData={reportData}
                showTotals={false}
                placeholderRows={10}
                placeholderColumns={3}
                showRowTitles={false}
                showTitle
            />
        )}
    </>
);

const DaysRequiredReport = ({ reportData, loading }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                <Title text="Production Requirements" />
            </Grid>
            <Grid item xs={12}>
                {loading || !reportData ? <Loading /> : <Results reportData={reportData} />}
            </Grid>
        </Grid>
    </Page>
);

Results.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({}))
};

Results.defaultProps = {
    reportData: []
};

DaysRequiredReport.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

DaysRequiredReport.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default DaysRequiredReport;
