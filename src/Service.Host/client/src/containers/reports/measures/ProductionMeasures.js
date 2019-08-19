import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ProductionMeasures from '../../../components/reports/measures/ProductionMeasures';
import actions from '../../../actions/productionMeasures';

import config from '../../../config';

import { getLoading, getCitsData } from '../../../selectors/productionMeasuresSelectors';

const mapStateToProps = state => ({
    citsData: getCitsData(state),
    loading: getLoading(state),
    config
});

const initialise = () => dispatch => {
    dispatch(actions.fetchReport());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ProductionMeasures));