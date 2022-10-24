import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, ExportButton, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import ProductionMeasuresInfo from './ProductionMeasuresInfo';
import ProductionMeasuresCits from './ProductionMeasuresCitTable';
import Page from '../../../containers/Page';

function ProductionMeasures({ loading, citsData, infoData, config }) {
    const href = `${config?.appRoot}/production/reports/measures/export`;

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={8}>
                    <Title text="Operations Status Report" />
                </Grid>
                <Grid item xs={4}>
                    <ExportButton href={href} />
                    <ProductionMeasuresInfo infoData={infoData} loading={loading} />
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                </Grid>
                {citsData && infoData ? (
                    <ProductionMeasuresCits
                        citsData={citsData}
                        infoData={infoData}
                        config={config}
                    />
                ) : (
                    <Loading />
                )}
            </Grid>
        </Page>
    );
}

ProductionMeasures.propTypes = {
    citsData: PropTypes.arrayOf(PropTypes.shape({})),
    infoData: PropTypes.shape({}),
    loading: PropTypes.bool,
    config: PropTypes.shape({ appRoot: PropTypes.string })
};

ProductionMeasures.defaultProps = {
    citsData: [],
    infoData: null,
    config: null,
    loading: false
};

export default ProductionMeasures;
