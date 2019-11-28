import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ProductionMeasures from '../../../components/reports/measures/ProductionMeasures';
import citActions from '../../../actions/productionMeasuresCits';
import infoActions from '../../../actions/productionMeasuresInfo';

import config from '../../../config';

import {
    getLoading,
    getCitsData,
    getInfoData
} from '../../../selectors/productionMeasuresSelectors';

const mapStateToProps = state => ({
    citsData: getCitsData(state),
    infoData: getInfoData(state),
    loading: getLoading(state),
    config
});

const initialise = () => dispatch => {
    dispatch(infoActions.fetchReport());
    dispatch(citActions.fetchReport());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ProductionMeasures));
