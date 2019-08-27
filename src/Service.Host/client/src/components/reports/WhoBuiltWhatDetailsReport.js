import React from 'react';
import { Loading, ReportTable, BackButton } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Box from '@material-ui/core/Box';
import Page from '../../containers/Page';

const handleBackClick = (history, options) => {
    history.push(
        `/production/reports/who-built-what/report?fromDate=${options.fromDate}&toDate=${options.toDate}&citCode=${options.citCode}`
    );
};

const WhoBuiltWhatDetailsReport = ({ reportData, loading, history, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={7}>
                <Box paddingBottom={3}>
                    {loading || !reportData ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            title={reportData.title}
                            showTitle
                            showTotals={false}
                            placeholderRows={4}
                            placeholderColumns={4}
                            showRowTitles={false}
                        />
                    )}
                </Box>
            </Grid>
            <Grid item xs={5} />
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
        </Grid>
    </Page>
);

WhoBuiltWhatDetailsReport.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

WhoBuiltWhatDetailsReport.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default WhoBuiltWhatDetailsReport;
