import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ManufacturingRoute from '../../components/productionTriggerLevels/TriggerLevel';
import productionTriggerLevelActions from '../../actions/productionTriggerLevelActions';
import productionTriggerLevelSelectors from '../../selectors/productionTriggerLevelSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: productionTriggerLevelSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: productionTriggerLevelSelectors.getEditStatus(state),
    loading: productionTriggerLevelSelectors.getLoading(state),
    snackbarVisible: productionTriggerLevelSelectors.getSnackbarVisible(state),
    parts: partsSelectors.getItems(state),
    manufacturingRoutes: manufacturingRoutesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    itemErrors: getItemError(state, itemTypes.productionTriggerLevel.item)
});

const initialise = () => dispatch => {
    dispatch(productionTriggerLevelActions.setEditStatus('create'));
    dispatch(partsActions.fetch());
    dispatch(manufacturingRoutesActions.fetch());
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: productionTriggerLevelActions.update,
    setEditStatus: productionTriggerLevelActions.setEditStatus,
    setSnackbarVisible: productionTriggerLevelActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(ManufacturingRoute));
