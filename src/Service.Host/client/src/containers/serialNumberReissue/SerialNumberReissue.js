import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import salesArticleActions from '../../actions/salesArticleActions';
import salesArticleSelectors from '../../selectors/salesArticleSelectors';
import SerialNumberReissue from '../../components/serialNumberReissue/SerialNumberReissue';
import serialNumberReissueActions from '../../actions/serialNumberReissueActions';
import serialNumberReissueSelectors from '../../selectors/serialNumberReissueSelectors';
import salesArticlesActions from '../../actions/salesArticlesActions';
import salesArticlesSelectors from '../../selectors/salesArticlesSelectors';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';

const mapStateToProps = state => ({
    editStatus: serialNumberReissueSelectors.getEditStatus(state),
    errorMessage: fetchErrorSelectors(state),
    item: serialNumberReissueSelectors.getItem(state),
    loading: serialNumberReissueSelectors.getLoading(state),
    salesArticle: salesArticleSelectors.getItem(state),
    salesArticleSearchResults: salesArticlesSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.articleNumber, name: s.articleNumber })),
    salesArticlesSearchLoading: salesArticlesSelectors.getSearchLoading(state),
    serialNumbers: serialNumberSelectors.getItems(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state),
    snackbarVisible: serialNumberReissueSelectors.getSnackbarVisible(state)
});

const mapDispatchToProps = {
    addItem: serialNumberReissueActions.add,
    fetchSerialNumbers: serialNumberActions.fetchByQueryString,
    fetchSalesArticle: salesArticleActions.fetch,
    searchSalesArticles: salesArticlesActions.search,
    clearSalesArticlesSearch: salesArticlesActions.clearSearch,
    setEditStatus: serialNumberReissueActions.setEditStatus,
    setSnackbarVisible: serialNumberReissueActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SerialNumberReissue);
