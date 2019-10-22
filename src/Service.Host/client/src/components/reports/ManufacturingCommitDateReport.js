import React, { Fragment } from 'react';
import { Loading, Title, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import moment from 'moment';
import Typography from '@material-ui/core/Typography';
import Page from '../../containers/Page';

const ManufacturingCommitDateReport = ({ reportData, loading, options }) => {
    if (loading) {
        return <Loading />;
    }

    if (reportData) {
        return (
            <Page>
                <Grid container spacing={3} justify="center">
                    <Grid item xs={12}>
                        <Title
                            text={`Manufacturing Commit Date Results For ${moment(options.date).format('DD-MMM-YYYY')}`}
                        />
                    </Grid>
                    {reportData.results.map(result => (
                        <Fragment>
                            <Grid item xs={12}>
                                <Typography variant="h5">
                                    Product Type: {result.productType ? result.productType : 'None'}
                                </Typography>
                            </Grid>
                            <Grid item xs={2}>
                                <Typography variant="h6">
                                    No Of Lines: {result.numberOfLines}
                                </Typography>
                            </Grid>
                            <Grid item xs={3}>
                                <Typography variant="h6">
                                    Supplied: {result.numberSupplied} ({result.percentageSupplied}%)
                                </Typography>
                            </Grid>
                            <Grid item xs={3}>
                                <Typography variant="h6">
                                    Available: {result.numberAvailable} (
                                    {result.percentageAvailable}%)
                                </Typography>
                            </Grid>
                            <Grid item xs={4} />
                            <Grid item xs={12}>
                                <ReportTable
                                    reportData={result.results.reportResults[0]}
                                    title={result.results.reportResults[0].title}
                                    showTitle={false}
                                    showTotals={false}
                                    placeholderRows={4}
                                    placeholderColumns={7}
                                    showRowTitles={false}
                                />
                            </Grid>
                        </Fragment>
                    ))}
                    <Grid item xs={12}>
                        <Typography variant="h5">Totals:</Typography>
                    </Grid>
                    <Grid item xs={2}>
                        <Typography variant="h6" gutterBottom>
                            No Of Lines: {reportData.totals.numberOfLines}
                        </Typography>
                    </Grid>
                    <Grid item xs={3}>
                        <Typography variant="h6">
                            Supplied: {reportData.totals.numberSupplied} ({reportData.totals.percentageSupplied}%)
                        </Typography>
                    </Grid>
                    <Grid item xs={3}>
                        <Typography variant="h6">
                            Available: {reportData.totals.numberAvailable} (
                            {reportData.totals.percentageAvailable}%)
                        </Typography>
                    </Grid>
                    <Grid item xs={4} />
                    <Grid item xs={6}>
                        <ReportTable
                            reportData={reportData.incompleteLinesAnalysis.reportResults[0]}
                            title={reportData.incompleteLinesAnalysis.reportResults[0].title}
                            showTitle
                            showTotals
                            placeholderRows={4}
                            placeholderColumns={2}
                            showRowTitles
                        />
                    </Grid>
                    <Grid item xs={6} />
                </Grid>
            </Page>
        );
    }

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Typography variant="body2">No report data found</Typography>
                </Grid>
            </Grid>
        </Page>
    );
};

ManufacturingCommitDateReport.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

ManufacturingCommitDateReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default ManufacturingCommitDateReport;
