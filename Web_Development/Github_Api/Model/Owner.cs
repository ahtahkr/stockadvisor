using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.Github_Api.Model
{
    public enum AccountType
    {
        User = 0,
        Organization = 1,
        Bot = 2
    }
    public class Owner
    {
        public object Permissions { get; set; }
        public bool SiteAdmin { get; set; }
        public object SuspendedAt { get; set; }
        public bool Suspended { get; set; }
        public object LdapDistinguishedName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AvatarUrl { get; set; }
        public object Bio { get; set; }
        public object Blog { get; set; }
        public object Collaborators { get; set; }
        public object Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public object DiskUsage { get; set; }
        public object Email { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public object Hireable { get; set; }
        public string HtmlUrl { get; set; }
        public int Id { get; set; }
        public object Location { get; set; }
        public string Login { get; set; }
        public object Name { get; set; }
        public int Type { get; set; }
        public int OwnedPrivateRepos { get; set; }
        public object Plan { get; set; }
        public object PrivateGists { get; set; }
        public int PublicGists { get; set; }
        public int PublicRepos { get; set; }
        public int TotalPrivateRepos { get; set; }
        public string Url { get; set; }
    }

    public class Author
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string FollowersUrl { get; set; }
        public string FollowingUrl { get; set; }
        public string GistsUrl { get; set; }
        public string StarredUrl { get; set; }
        public string SubscriptionsUrl { get; set; }
        public string OrganizationsUrl { get; set; }
        public string ReposUrl { get; set; }
        public string EventsUrl { get; set; }
        public string ReceivedEventsUrl { get; set; }
        public string Type { get; set; }
        public bool SiteAdmin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Tree
    {
        public string Url { get; set; }
        public object Label { get; set; }
        public object Ref { get; set; }
        public string Sha { get; set; }
        public object User { get; set; }
        public object Repository { get; set; }
    }

    public class Reason
    {
        public string StringValue { get; set; }
        public int Value { get; set; }
    }

    public class Verification
    {
        public bool Verified { get; set; }
        public Reason Reason { get; set; }
        public string Signature { get; set; }
        public string Payload { get; set; }
    }

    public class Commit
    {
        public string Message { get; set; }
        public Author Author { get; set; }
        public Author Committer { get; set; }
        public Tree Tree { get; set; }
        public object Parents { get; set; }
        public int CommentCount { get; set; }
        public Verification Verification { get; set; }
        public string Url { get; set; }
        public object Label { get; set; }
        public object Ref { get; set; }
        public object Sha { get; set; }
        public object User { get; set; }
        public object Repository { get; set; }
    }

    public class CommitEvent
    {
        public Author Author { get; set; }
        public string CommentsUrl { get; set; }
        public Commit Commit { get; set; }
        public Author Committer { get; set; }
        public string HtmlUrl { get; set; }
        public object Stats { get; set; }
        public List<Tree> Parents { get; set; }
        public object Files { get; set; }
        public string Url { get; set; }
        public object Label { get; set; }
        public object Ref { get; set; }
        public string Sha { get; set; }
        public object User { get; set; }
        public object Repository { get; set; }
    }

    public class License
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string SpdxId { get; set; }
        public string Url { get; set; }
        public bool Featured { get; set; }
    }

    public class Repository
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string GitUrl { get; set; }
        public string SshUrl { get; set; }
        public string SvnUrl { get; set; }
        public object MirrorUrl { get; set; }
        public int Id { get; set; }
        public Owner Owner { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public object Homepage { get; set; }
        public string Language { get; set; }
        public bool Private { get; set; }
        public bool Fork { get; set; }
        public int ForksCount { get; set; }
        public int StargazersCount { get; set; }
        public string DefaultBranch { get; set; }
        public int OpenIssuesCount { get; set; }
        public DateTime PushedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object Permissions { get; set; }
        public object Parent { get; set; }
        public object Source { get; set; }
        public License License { get; set; }
        public bool HasIssues { get; set; }
        public bool HasWiki { get; set; }
        public bool HasDownloads { get; set; }
        public object AllowRebaseMerge { get; set; }
        public object AllowSquashMerge { get; set; }
        public object AllowMergeCommit { get; set; }
        public bool HasPages { get; set; }
        public int SubscribersCount { get; set; }
        public int Size { get; set; }

        public string Api_String { get; set; }
    }
}
