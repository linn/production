import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFailsWaitingListReport({ reportData, loading, errorMessage }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                <Grid item xs={8}>
                    <Title text="Assembly Fails Waiting List" />
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                    <ReportTable
                        reportData={reportData}
                        showTotals={false}
                        placeholderRows={10}
                        placeholderColumns={3}
                        showTitle={false}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFailsWaitingListReport.propTypes = {
    reportData: PropTypes.shape({}),
    config: PropTypes.shape({}),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

AssemblyFailsWaitingListReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false,
    errorMessage: ''
};

export default AssemblyFailsWaitingListReport;
