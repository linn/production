import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import TriggerLevel from '../../components/productionTriggerLevels/TriggerLevel';
import productionTriggerLevelActions from '../../actions/productionTriggerLevelActions';
import productionTriggerLevelSelectors from '../../selectors/productionTriggerLevelSelectors';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';
import employeesActions from '../../actions/employeesActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import workStationActions from '../../actions/workStationActions';
import workStationSelectors from '../../selectors/workStationSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: productionTriggerLevelSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: productionTriggerLevelSelectors.getEditStatus(state),
    loading: productionTriggerLevelSelectors.getLoading(state),
    snackbarVisible: productionTriggerLevelSelectors.getSnackbarVisible(state),
    parts: [],
    manufacturingRoutes: manufacturingRoutesSelectors.getItems(state),
    cits: citsSelectors.getItems(state),
    employees: employeesSelectors.getItems(state),
    itemErrors: getItemError(state, itemTypes.productionTriggerLevel.item),
    workStations: workStationSelectors.getItems(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(productionTriggerLevelActions.fetch(itemId));
    dispatch(manufacturingRoutesActions.fetch(''));
    dispatch(citsActions.fetch());
    dispatch(employeesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: productionTriggerLevelActions.update,
    setEditStatus: productionTriggerLevelActions.setEditStatus,
    setSnackbarVisible: productionTriggerLevelActions.setSnackbarVisible,
    getWorkStationsForCit: workStationActions.fetchByQueryString
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TriggerLevel));
