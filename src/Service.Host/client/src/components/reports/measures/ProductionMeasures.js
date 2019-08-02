import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title, ExportButton } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import ProductionMeasuresCits from './ProductionMeasuresCits';
import Page from '../../../containers/Page';

function ProductionMeasures({ loading, citsData, config }) {
    const href = `${config.appRoot}/production/reports/measures/export`;
    const [tabValue, setValue] = React.useState(0);

    function handleChange(event, newValue) {
        setValue(newValue);
    }

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                <Grid item xs={8}>
                    <Title text="Operations Status Report" />
                </Grid>
                <Grid item xs={4}>
                    <ExportButton href={href} />
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                </Grid>
                {citsData ? <ProductionMeasuresCits citsData={citsData} /> : 'No'}
            </Grid>
        </Page>
    );
}

ProductionMeasures.propTypes = {
    citsData: PropTypes.shape({}),
    loading: PropTypes.bool
};

ProductionMeasures.defaultProps = {
    citsData: null,
    loading: false
};

export default ProductionMeasures;
