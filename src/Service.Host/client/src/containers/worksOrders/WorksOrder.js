import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import initialiseOnMount from '../initialiseOnMount';
import WorksOrder from '../../components/worksOrders/WorksOrder';
import worksOrderSelectors from '../../selectors/worksOrderSelectors';
import worksOrderActions from '../../actions/worksOrderActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import employeesActions from '../../actions/employeesActions';
import worksOrderDetailsActions from '../../actions/worksOrderDetailsActions';
import worksOrderDetailsSelectors from '../../selectors/worksOrderDetailsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: worksOrderSelectors.getItem(state),
    orderNumber: match.params.id,
    worksOrderError: getItemErrors(state, itemTypes.worksOrder.item),
    worksOrderDetailsError: getItemErrors(state, itemTypes.worksOrderDetails.item),
    editStatus: worksOrderSelectors.getEditStatus(state),
    loading: worksOrderSelectors.getLoading(state),
    snackbarVisible: worksOrderSelectors.getSnackbarVisible(state),
    employees: employeesSelectors.getItems(state),
    employeeesLoading: employeesSelectors.getLoading(state),
    worksOrderDetails: worksOrderDetailsSelectors.getItem(state)
});

const initialise = ({ orderNumber }) => dispatch => {
    if (orderNumber) {
        dispatch(worksOrderActions.fetch(orderNumber));
    }
    dispatch(employeesActions.fetch());
    dispatch(worksOrderDetailsActions.reset());
};

const mapDispatchToProps = {
    initialise,
    setSnackbarVisible: worksOrderActions.setSnackbarVisible,
    fetchWorksOrderDetails: worksOrderDetailsActions.fetch,
    addItem: worksOrderActions.add,
    updateItem: worksOrderActions.update,
    setEditStatus: worksOrderActions.setEditStatus,
    fetchWorksOrder: worksOrderActions.fetch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorksOrder));
