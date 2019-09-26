import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, BackButton, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function BuildsSummaryReport({ reportData, loading, history }) {
    const handleBackClick = () => {
        history.push('/production/reports/builds-summary-options');
    };
    return (
        <Page>
            <Grid container spacing={3} justify="center">
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
                            showRowTitles={false}
                        />
                    )}
                </Grid>
                <Grid item xs={12}>
                    <BackButton backClick={() => handleBackClick(history)} />
                </Grid>
            </Grid>
        </Page>
    );
}

BuildsSummaryReport.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

BuildsSummaryReport.defaultProps = {
    reportData: null,
    loading: false,
    errorMessage: ''
};

export default BuildsSummaryReport;
