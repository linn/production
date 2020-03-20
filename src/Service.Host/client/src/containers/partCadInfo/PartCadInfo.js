import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import partCadInfoActions from '../../actions/partCadInfoActions';
import partCadInfoSelectors from '../../selectors/partCadInfoSelectors';
import PartCadInfo from '../../components/partCadInfo/PartCadInfo';

const mapStateToProps = state => ({
    loading: partCadInfoSelectors.getLoading(state),
    item: partCadInfoSelectors.getItem(state),
    snackbarVisible: partCadInfoSelectors.getSnackbarVisible(state),
    itemErrors: getItemErrors(state),
    editStatus: partCadInfoSelectors.getEditStatus(state)
});

const mapDispatchToProps = {
    fetchPartCadInfo: partCadInfoActions.fetch,
    updatePartCadInfo: partCadInfoActions.update,
    setSnackbarVisible: partCadInfoActions.setSnackbarVisible,
    setEditStatus: partCadInfoActions.setEditStatus
};

export default connect(mapStateToProps, mapDispatchToProps)(PartCadInfo);
