import React from 'react';
import {
    Loading,
    ReportTable,
    BackButton,
    ExportButton
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const handleBackClick = (history, options) => {
    let uri = `/production/reports/assembly-fails-measures/report?fromDate=${encodeURIComponent(
        options.fromDate
    )}&toDate=${encodeURIComponent(options.toDate)}`;
    if (options.parentGroupBy) {
        uri += `&groupBy=${options.parentGroupBy}`;
    }

    history.push(uri);
};

const AssemblyFailsDetails = ({ reportData, loading, history, options, config }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                {!loading && reportData ? (
                    <ExportButton
                        href={`${config.appRoot}/production/reports/assembly-fails-details/report/export?fromDate=${options.fromDate}&toDate=${options.toDate}`}
                    />
                ) : (
                    ''
                )}
            </Grid>
            <Grid item xs={12}>
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
                        showRowTitles
                    />
                )}
            </Grid>
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history, options)} />
            </Grid>
        </Grid>
    </Page>
);

AssemblyFailsDetails.propTypes = {
    reportData: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({}),
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired
};

AssemblyFailsDetails.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default AssemblyFailsDetails;
