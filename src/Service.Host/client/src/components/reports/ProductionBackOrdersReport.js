import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import Switch from '@material-ui/core/Switch';
import { MultiReportTable, Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import '../../../assets/printStyles.css';

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
                        <div class="zoomed-in-printing">
                            <MultiReportTable
                                reportData={reportData}
                                showRowTitles={false}
                                showTotals
                            />
                        </div>
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

ProductionBackOrdersReport.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    error: PropTypes.string
};

ProductionBackOrdersReport.defaultProps = {
    reportData: null,
    loading: false,
    error: ''
};
