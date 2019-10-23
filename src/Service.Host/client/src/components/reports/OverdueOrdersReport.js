import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ReportTable, Loading, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

// TODO get report by and change sub text
export default function OverdueOrdersReport({ reportData, loading, config }) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={8}>
                    <Title text="Outstanding Sales Orders by Days Late" />
                    {/* TODO subtext */}
                </Grid>
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <ReportTable
                            reportData={reportData}
                            showTotals={false}
                            showTitle={false}
                            showRowTitles
                        />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}
