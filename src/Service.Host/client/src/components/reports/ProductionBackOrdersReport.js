import React, { useState, useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import Switch from '@material-ui/core/Switch';
import { MultiReportTable, Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import '../../../assets/printStyles.css';

export default function ProductionBackOrdersReport({ reportData, loading, error }) {
    const [extraRows, setExtraRows] = useState(false);

    const updateExtraRows = () => {
        setExtraRows(!extraRows);
    };

    let extraRowsData = { ...reportData };

    useEffect(() => {
        if (reportData) {
            console.info(extraRowsData);
            extraRowsData.forEach(section => {
                console.info(section);
            });
        }
    }, [reportData, extraRowsData]);

    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={9}>
                    <Title text="Production Back Orders" />
                </Grid>
                <Grid item xs={3}>
                    <Switch
                        aria-label="Add extra rows for printing"
                        value={extraRows}
                        onChange={updateExtraRows}
                        color="primary"
                    />
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
                        <div class="zoomed-out-production-back-orders">
                            <MultiReportTable
                                reportData={extraRows ? extraRowsData : reportData}
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
