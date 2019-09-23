import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, Title, ExportButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function OutstandingWorksOrdersReport({ reportData, loading, config }) {
    const href = `${config.appRoot}/production/maintenance/works-orders/outstanding-works-orders-report/export`;

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={8}>
                    <Title text="Outstanding Works Orders" />
                </Grid>
                {!loading && (
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
    loading: PropTypes.bool
};

OutstandingWorksOrdersReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false
};

export default OutstandingWorksOrdersReport;
