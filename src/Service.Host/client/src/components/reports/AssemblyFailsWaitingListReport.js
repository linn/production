import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFailsWaitingListReport({ reportData, loading }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={8}>
                    <Title text="Assembly Fails Waiting List" />
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                    {reportData && (
                        <ReportTable
                            reportData={reportData}
                            showRowTitles
                            showTotals={false}
                            placeholderRows={10}
                            placeholderColumns={3}
                            showTitle={false}
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFailsWaitingListReport.propTypes = {
    reportData: PropTypes.shape({}),
    config: PropTypes.shape({}),
    loading: PropTypes.bool
};

AssemblyFailsWaitingListReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false
};

export default AssemblyFailsWaitingListReport;
