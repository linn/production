import React from 'react';
import {
    Loading,
    BackButton,
    ReportTable,
    ErrorCard,
    Title
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const handleBackClick = history => {
    history.push('/production/reports/build-plans');
};

export default function BoardTestsReport({ reportData, loading, history, error }) {
    return (
        <Page width="xl">
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text="Build Plan Report" />
                </Grid>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    {loading || !reportData ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            title={reportData.title}
                            showTitle
                            showTotals={false}
                            placeholderRows={3}
                            placeholderColumns={6}
                            showRowTitles
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

BoardTestsReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({}),
    error: PropTypes.string
};

BoardTestsReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false,
    error: ''
};
