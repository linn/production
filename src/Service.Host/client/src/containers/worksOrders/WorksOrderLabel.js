import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import WorksOrderLabel from '../../components/worksOrders/WorksOrderLabel';
import worksOrderLabelActions from '../../actions/worksOrderLabelActions';
import worksOrderLabelSelectors from '../../selectors/worksOrderLabelSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: worksOrderLabelSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: worksOrderLabelSelectors.getEditStatus(state),
    loading: worksOrderLabelSelectors.getLoading(state),
    snackbarVisible: worksOrderLabelSelectors.getSnackbarVisible(state),
    itemError: getItemError(state, itemTypes.worksOrderLabel.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(worksOrderLabelActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: worksOrderLabelActions.update,
    setEditStatus: worksOrderLabelActions.setEditStatus,
    setSnackbarVisible: worksOrderLabelActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorksOrderLabel));
