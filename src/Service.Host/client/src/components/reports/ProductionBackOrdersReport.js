import React from 'react';
import Grid from '@material-ui/core/Grid';
import { MultiReportTable, Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function ProductionBackOrdersReport({ reportData, loading, error }) {
    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Production Back Orders" />
                </Grid>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <MultiReportTable
                            reportData={reportData}
                            showRowTitles={false}
                            showTotals
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

ProductionBackOrdersReport.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    error: PropTypes.string
};

ProductionBackOrdersReport.defaultProps = {
    reportData: null,
    loading: false,
    error: ''
};
