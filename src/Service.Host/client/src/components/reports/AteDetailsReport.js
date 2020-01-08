import React from 'react';
import { Loading, ReportTable, BackButton } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Box from '@material-ui/core/Box';
import Page from '../../containers/Page';

const handleBackClick = (history, options) => {
    let uri = `/production/reports/ate/status/report?fromDate=${encodeURIComponent(
        options.fromDate
    )}&toDate=${encodeURIComponent(options.toDate)}`;
    if (options.parentGroupBy) {
        uri += `&groupBy=${options.parentGroupBy}`;
    }

    if (options.placeFound) {
        uri += `&placeFound=${options.placeFound}`;
    }

    if (options.smtOrPcb) {
        uri += `&smtOrPcb=${options.smtOrPcb}`;
    }

    history.push(uri);
};

const AteDetailsReport = ({ reportData, loading, history, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                <Box paddingBottom={3}>
                    {loading || !reportData ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            title={reportData.title}
                            showTitle
                            showTotals
                            placeholderRows={4}
                            placeholderColumns={4}
                            showRowTitles={false}
                        />
                    )}
                </Box>
            </Grid>
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
        </Grid>
    </Page>
);

AteDetailsReport.propTypes = {
    reportData: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

AteDetailsReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default AteDetailsReport;
