import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import ProductionMeasures from '../../../components/reports/measures/ProductionMeasures';
import actions from '../../../actions/productionMeasures';
import { getCitsData, getReportLoading } from '../../../selectors/productionMeasureSelectors';
import config from '../../../config';

const mapStateToProps = state => ({
    citsData: getCitsData(state),
    loading: getReportLoading(state),
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