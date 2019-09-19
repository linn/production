import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import initialiseOnMount from '../initialiseOnMount';
import WorksOrder from '../../components/worksOrders/WorksOrder';
import worksOrderSelectors from '../../selectors/worksOrderSelectors';
import worksOrderActions from '../../actions/worksOrderActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import employeesActions from '../../actions/employeesActions';
import worksOrderDetailsActions from '../../actions/worksOrderDetailsActions';
import worksOrderDetailsSelectors from '../../selectors/worksOrderDetailsSelectors';
import departmentActions from '../../actions/departmentActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';

const mapStateToProps = (state, { match }) => ({
    item: worksOrderSelectors.getItem(state),
    orderNumber: match.params.id,
    errorMessage: fetchErrorSelectors(state),
    editStatus: worksOrderSelectors.getEditStatus(state),
    loading: worksOrderSelectors.getLoading(state),
    snackbarVisible: worksOrderSelectors.getSnackbarVisible(state),
    employees: employeesSelectors.getItems(state),
    worksOrderDetails: worksOrderDetailsSelectors.getItem(state),
    departments: departmentsSelectors.getItems(state),
    departmentsLoading: departmentsSelectors.getLoading(state)
});

const initialise = ({ orderNumber }) => dispatch => {
    if (orderNumber) {
        dispatch(worksOrderActions.fetch(orderNumber));
    }
    dispatch(employeesActions.fetch());
    dispatch(worksOrderDetailsActions.reset());
    dispatch(departmentActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    setSnackbarVisible: worksOrderActions.setSnackbarVisible,
    fetchWorksOrderDetails: worksOrderDetailsActions.fetch,
    addItem: worksOrderActions.add,
    setEditStatus: worksOrderActions.setEditStatus,
    fetchWorksOrder: worksOrderActions.fetch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorksOrder));
