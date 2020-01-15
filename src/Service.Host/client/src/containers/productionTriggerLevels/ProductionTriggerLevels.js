import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ViewManufacturingRoutes from '../../components/productionTriggerLevels/TriggerLevels';
import productionTriggerLevelsActions from '../../actions/productionTriggerLevelsActions';
import productionTriggerLevelsSelectors from '../../selectors/productionTriggerLevelsSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import * as itemTypes from '../../itemTypes';
import productionTriggerLevelsStateActions from '../../actions/productionTriggerLevelsStateActions';
import productionTriggerLevelSelectors from '../../selectors/productionTriggerLevelSelectors';

const mapStateToProps = state => ({
    items: productionTriggerLevelsSelectors.getItems(state),
    loading: productionTriggerLevelsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.productionTriggerLevels.item),
    cits: citsSelectors.getItems(state),
    applicationState: productionTriggerLevelsSelectors.getApplicationState(state),
    editStatus: productionTriggerLevelSelectors.getEditStatus(state)
});

const initialise = () => dispatch => {
    dispatch(citsActions.fetch());
    dispatch(productionTriggerLevelsStateActions.fetchState());
};
const mapDispatchToProps = {
    fetchItems: productionTriggerLevelsActions.fetchByQueryString,
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewManufacturingRoutes));
