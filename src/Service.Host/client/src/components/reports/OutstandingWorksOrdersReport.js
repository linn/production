import React from 'react';
import Grid from '@material-ui/core/Grid';
import {
    ReportTable,
    Loading,
    Title,
    ExportButton,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function OutstandingWorksOrdersReport({ reportData, loading, config, errorMessage }) {
    const href = `${config.appRoot}/production/works-orders/outstanding-works-orders-report/export`;

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                <Grid item xs={8}>
                    <Title text="Outstanding Works Orders" />
                </Grid>
                {!loading && !errorMessage && (
                    <Grid item xs={4}>
                        <ExportButton href={href} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            showTotals={false}
                            placeholderRows={10}
                            placeholderColumns={3}
                            showTitle={false}
                            showRowTitles
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

OutstandingWorksOrdersReport.propTypes = {
    reportData: PropTypes.shape({}),
    config: PropTypes.shape({}),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

OutstandingWorksOrdersReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false,
    errorMessage: ''
};

export default OutstandingWorksOrdersReport;
